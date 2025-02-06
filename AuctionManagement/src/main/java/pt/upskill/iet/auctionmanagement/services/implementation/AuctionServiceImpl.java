package pt.upskill.iet.auctionmanagement.services.implementation;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.reactive.function.client.WebClientResponseException;
import pt.upskill.iet.auctionmanagement.dto.AuctionDTO;
import pt.upskill.iet.auctionmanagement.dto.ItemDTO;
import pt.upskill.iet.auctionmanagement.dto.ItemStatusDTO;
import pt.upskill.iet.auctionmanagement.dto.SaleDTO;
import pt.upskill.iet.auctionmanagement.exceptions.ResourceNotFoundException;
import pt.upskill.iet.auctionmanagement.models.Auction;
import pt.upskill.iet.auctionmanagement.models.Bid;
import pt.upskill.iet.auctionmanagement.repositories.AuctionRepository;
import pt.upskill.iet.auctionmanagement.repositories.BidRepository;
import pt.upskill.iet.auctionmanagement.services.AuctionService;
import pt.upskill.iet.auctionmanagement.services.ItemService;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.Optional;
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
            ItemDTO itemDetails = itemService.getItemDetails(auctionDTO.getItemId()).block();

            if (itemDetails == null) {
                throw new ResourceNotFoundException("Item não encontrado na API externa.");
            }

            // Atualiza o status do item na API externa
            itemService.updateItemStatus(itemDetails.getId(), ItemStatusDTO.Pending).block();

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
        Optional<Auction> auctionOptional = auctionRepository.findById(auctionId);
        if (auctionOptional.isPresent()) {
            return AuctionDTO.fromAuctionToDto(auctionOptional.get());
        }
        throw new ResourceNotFoundException("Leilão não encontrado com id " + auctionId);
    }

    // Obter todos os leilões
    @Override
    public List<AuctionDTO> getAllAuctions() {
        List<Auction> auctions = auctionRepository.findAll();
        LocalDate today = LocalDate.now();

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
                    ItemDTO itemDetails = itemService.getItemDetails(auction.getItemId()).block();
                    if (itemDetails != null && itemDetails.getItemStatus() == ItemStatusDTO.Pending) {
                        itemService.updateItemStatus(itemDetails.getId(), ItemStatusDTO.Available).block();
                    }
                }
            }
        }

        return auctions.stream()
                .map(AuctionDTO::fromAuctionToDto)  // Convertendo cada Auction para AuctionDTO
                .collect(Collectors.toList());
    }

    // Atualizar um leilão existente
    @Override
    public AuctionDTO updateAuction(long auctionId, AuctionDTO auctionDTO) {
        Optional<Auction> auctionOptional = auctionRepository.findById(auctionId);
        if (auctionOptional.isPresent()) {
            Auction auction = auctionOptional.get();
            // Atualizando os campos do leilão com os dados do AuctionDTO
            auction.setStartDate(auctionDTO.getStartDate());
            auction.setFinalDate(auctionDTO.getFinalDate());
            auction.setOpen(auctionDTO.isOpen());
            auction = auctionRepository.save(auction);  // Salvando as alterações no banco

            return AuctionDTO.fromAuctionToDto(auction);
        }
        throw new ResourceNotFoundException("Leilão não encontrado com id " + auctionId);
    }

    // Excluir um leilão
    @Override
    public void deleteAuction(long auctionId) {
        Optional<Auction> auctionOptional = auctionRepository.findById(auctionId);
        if (auctionOptional.isPresent()) {
            Auction auction = auctionOptional.get();
            ItemDTO itemDetails = itemService.getItemDetails(auction.getItemId()).block();

            if (itemDetails != null && itemDetails.getItemStatus() == ItemStatusDTO.Pending) {
                itemService.updateItemStatus(itemDetails.getId(), ItemStatusDTO.Available).block();
            }

            auctionRepository.delete(auction);  // Excluindo o leilão do banco
        } else {
            throw new ResourceNotFoundException("Leilão não encontrado com id " + auctionId);
        }
    }
}


