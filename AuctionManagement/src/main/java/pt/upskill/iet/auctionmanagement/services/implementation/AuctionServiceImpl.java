package pt.upskill.iet.auctionmanagement.services.implementation;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.reactive.function.client.WebClientResponseException;
import pt.upskill.iet.auctionmanagement.dto.*;
import pt.upskill.iet.auctionmanagement.exceptions.ResourceNotFoundException;
import pt.upskill.iet.auctionmanagement.models.Auction;
import pt.upskill.iet.auctionmanagement.models.Bid;
import pt.upskill.iet.auctionmanagement.repositories.AuctionRepository;
import pt.upskill.iet.auctionmanagement.repositories.BidRepository;
import pt.upskill.iet.auctionmanagement.services.AuctionService;
import pt.upskill.iet.auctionmanagement.services.ItemService;
import reactor.core.publisher.Flux;

import java.time.LocalDate;
import java.util.*;
import java.util.stream.Collectors;

@Service
public class AuctionServiceImpl implements AuctionService {

    private final AuctionRepository auctionRepository;
    private final ItemService itemService;
    private final BidRepository bidRepository;

    @Autowired
    public AuctionServiceImpl(AuctionRepository auctionRepository, ItemService itemService, BidRepository bidRepository) {
        this.auctionRepository = auctionRepository;
        this.itemService = itemService;
        this.bidRepository = bidRepository;
    }

    // Criar um novo leilão
    @Override
    public AuctionDTO createAuction(AuctionDTO auctionDTO) {
        try {
            // Obtém os detalhes do item da API externa
            ItemDTO itemDetails = itemService.getItemDetails(auctionDTO.getItemId());
            System.out.println(itemDetails.toString());

            if (itemDetails == null) {
                throw new ResourceNotFoundException("Item não encontrado na API externa.");
            }

            // Atualiza o status do item na API externa
            itemService.updateItemStatus(itemDetails.getId(), ItemStatusDTO.Pending);

            // Criar e salvar o Auction
            Auction auction = new Auction();
            auction.setItemId(auctionDTO.getItemId());
            auction.setStartDate(auctionDTO.getStartDate());
            auction.setFinalDate(auctionDTO.getFinalDate());
            auction.setOpen(true);
            auction = auctionRepository.save(auction);
            return AuctionDTO.fromAuctionToDto(auction);

        } catch (WebClientResponseException e) {
            throw new RuntimeException("Erro ao consumir API externa: " + e.getMessage());
        } catch (Exception e) {
            throw new RuntimeException("Erro ao criar leilão: " + e.getMessage());
        }
    }

    // Obter um leilão por ID
    @Override
    public AuctionDTO getAuctionById(long auctionId) {
        closeAuctions();
        Optional<Auction> auctionOptional = auctionRepository.findById(auctionId);
        if (auctionOptional.isPresent()) {
            return AuctionDTO.fromAuctionToDto(auctionOptional.get());
        }
        throw new ResourceNotFoundException("Leilão não encontrado com id " + auctionId);
    }

    // Obter todos os leilões
    @Override
    public List<AuctionDTO> getAllAuctions() {
        closeAuctions();
        List<Auction> auctions = auctionRepository.findAll();
        return auctions.stream()
                .map(AuctionDTO::fromAuctionToDto)  // Convertendo cada Auction para AuctionDTO
                .collect(Collectors.toList());
    }

//    // Atualizar um leilão existente
//    @Override
//    public AuctionDTO updateAuction(long auctionId, AuctionDTO auctionDTO) {
//        closeAuctions();
//        Optional<Auction> auctionOptional = auctionRepository.findById(auctionId);
//        if (auctionOptional.isPresent()) {
//            Auction auction = auctionOptional.get();
//            // Atualizando os campos do leilão com os dados do AuctionDTO
//            auction.setStartDate(auctionDTO.getStartDate());
//            auction.setFinalDate(auctionDTO.getFinalDate());
//            auction.setOpen(auctionDTO.isOpen());
//            auction = auctionRepository.save(auction);  // Salvando as alterações no banco
//
//            return AuctionDTO.fromAuctionToDto(auction);
//        }
//        throw new ResourceNotFoundException("Leilão não encontrado com id " + auctionId);
//    }

    // Excluir um leilão
    @Override
    public void deleteAuction(long auctionId) {
        closeAuctions();
        Optional<Auction> auctionOptional = auctionRepository.findById(auctionId);
        if(!auctionOptional.get().isOpen() && auctionOptional.get().getFinalDate().isBefore(LocalDate.now())){
            throw new ResourceNotFoundException("Leilão não pode ser excluído pois já foi encerrado.");
        }
        Optional<Bid> highestBid = bidRepository.findTopByAuctionOrderByBidAmountDesc(auctionOptional.get());

        if(highestBid.isPresent()){
            throw new ResourceNotFoundException("Leilão não pode ser excluído pois já possui lances.");
        }

        if (auctionOptional.isPresent()) {
            Auction auction = auctionOptional.get();
            ItemDTO itemDetails = itemService.getItemDetails(auction.getItemId());

            if (itemDetails != null && itemDetails.getItemStatus() == ItemStatusDTO.Pending) {
                itemService.updateItemStatus(itemDetails.getId(), ItemStatusDTO.Available);
            }

            auctionRepository.delete(auction);  // Excluindo o leilão do banco
        } else {
            throw new ResourceNotFoundException("Leilão não encontrado com id " + auctionId);
        }
    }

    @Override
    public List<AuctionDTO> getAuctionsByClient(long clientId) {
        List<Auction> auctions = bidRepository.findAuctionsByClientId(clientId);
        return auctions.stream()
                .map(AuctionDTO::fromAuctionToDto)
                .collect(Collectors.toList());
    }

    @Override
    public List<AuctionDTO> getWonAuctionsByClient(long clientId) {
        List<Auction> wonAuctions = bidRepository.findWonAuctionsByClientId(clientId);
        return wonAuctions.stream()
                .map(AuctionDTO::fromAuctionToDto)
                .collect(Collectors.toList());
    }

    @Override
    public List<ItemDTO> getAvailableItems() {
        closeAuctions();
        return  Arrays.stream(itemService.getItemsAvailable()).toList();
    }


    public void closeAuctions() {
        List<Auction> auctions = auctionRepository.findAll();
        LocalDate today = LocalDate.now();
        if(!auctions.isEmpty()){
            for (Auction auction : auctions) {
                if (auction.getFinalDate().isBefore(today) && auction.isOpen()) {
                    auction.setOpen(false);
                    auctionRepository.save(auction); // Atualiza no banco

                    // Busca o lance de maior valor no banco de dados para este leilão

                    Optional<Bid> highestBid = bidRepository.findTopByAuctionOrderByBidAmountDesc(auction);

                    if (highestBid.isPresent()) {
                        SaleDTO sale = new SaleDTO(auction.getItemId(), highestBid.get().getBidAmount());
                        itemService.createSale(sale);
                    } else {
                        // Caso não tenha lances, atualiza o status do item para disponível
                        ItemDTO itemDetails = itemService.getItemDetails(auction.getItemId());
                        if (itemDetails != null && itemDetails.getItemStatus() == ItemStatusDTO.Pending) {
                            itemService.updateItemStatus(itemDetails.getId(), ItemStatusDTO.Available);
                        }
                    }
                }
            }
        }
    }
}


