package pt.upskill.iet.auctionmanagement.services.implementation;

import org.springframework.stereotype.Service;
import org.springframework.web.client.ResourceAccessException;
import pt.upskill.iet.auctionmanagement.dto.BidDTO;
import pt.upskill.iet.auctionmanagement.dto.ItemDTO;
import pt.upskill.iet.auctionmanagement.exceptions.ResourceNotFoundException;
import pt.upskill.iet.auctionmanagement.models.Auction;
import pt.upskill.iet.auctionmanagement.models.Bid;
import pt.upskill.iet.auctionmanagement.models.Client;
import pt.upskill.iet.auctionmanagement.models.Item;
import pt.upskill.iet.auctionmanagement.repositories.AuctionRepository;
import pt.upskill.iet.auctionmanagement.repositories.BidRepository;
import pt.upskill.iet.auctionmanagement.repositories.ClientRepository;
import pt.upskill.iet.auctionmanagement.services.BidService;
import pt.upskill.iet.auctionmanagement.services.ItemService;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
public class BidServiceImpl implements BidService {
    private final BidRepository bidRepository;
    private final AuctionRepository auctionRepository;
    private final ClientRepository clientRepository;
    private final ItemService itemService;


    public BidServiceImpl(BidRepository bidRepository, AuctionRepository auctionRepository, ClientRepository clientRepository, ItemService itemService) {
        this.bidRepository = bidRepository;
        this.auctionRepository = auctionRepository;
        this.clientRepository = clientRepository;
        this.itemService = itemService;
    }

    @Override
    public void createBid(BidDTO bidDTO) {
        if (bidDTO.getBidAmount() <= 0) {
            throw new IllegalArgumentException("Bid amount must be greater than zero.");
        }

        Optional<Auction> optionalAuction = auctionRepository.findById(bidDTO.getAuctionId());
        Optional<Client> optionalClient = clientRepository.findById(bidDTO.getClientId());

        if (optionalAuction.isEmpty() || optionalClient.isEmpty()) {
            throw new ResourceAccessException("Auction or Client not found.");
        }

        ItemDTO itemDetails = itemService.getItemDetails(optionalAuction.get().getItemId()).block();

        assert itemDetails != null;
        if (bidDTO.getBidAmount() < itemDetails.getInitialPrice()) {
            throw new IllegalArgumentException("Bid amount must be greater than the Item base price: " + itemDetails.getInitialPrice());
        }



        Auction auction = optionalAuction.get();
        Client client = optionalClient.get();

        // Verifica se a data do lance é anterior à data final do leilão
        if (bidDTO.getBidDate().isAfter(auction.getFinalDate())) {
            throw new IllegalArgumentException("The bid date must be before the auction's final date.");
        }

        // Busca no banco de dados o maior lance para este leilão
        Optional<Bid> lastBid = bidRepository.findTopByAuctionOrderByBidAmountDesc(auction);


        if (lastBid.isPresent() && bidDTO.getBidAmount() <= lastBid.get().getBidAmount()) {
            throw new IllegalArgumentException("The bid amount must be greater than the last bid.");
        }

        // Cria o Bid e associa ao Auction e ao Client
        Bid bid = new Bid();
        bid.setAuction(auction);
        bid.setClient(client);
        bid.setBidAmount(bidDTO.getBidAmount());
        bid.setBidDate(bidDTO.getBidDate());

        // Salva o lance
        bidRepository.save(bid);
    }

    @Override
    public List<BidDTO> getAllBids() {
        return bidRepository.findAll().stream()
                .map(BidDTO::fromBidToDto)
                .collect(Collectors.toList());
    }

    @Override
    public BidDTO getBidById(Long id) {
        return bidRepository.findById(id)
                .map(BidDTO::fromBidToDto)
                .orElseThrow(() -> new IllegalArgumentException("Bid not found"));
    }

    @Override
    public BidDTO updateBid(Long id, BidDTO bidDTO) {
        Optional<Bid> optionalBid = this.bidRepository.findById(id);
        Optional<Client> user = clientRepository.findById(bidDTO.getClientId());
        Optional<Auction> auction = auctionRepository.findById(bidDTO.getAuctionId());
        if (auction.isEmpty()) {
            throw new ResourceAccessException("Auction not found");
        }

        if (optionalBid.isEmpty()) {
            throw new ResourceAccessException("Bid not found");
        }
        if (user.isEmpty()) {
            throw new ResourceAccessException("User not found");
        }

        Bid bid = BidDTO.fromDtoToBid(auctionRepository, clientRepository, bidDTO);
        bid.setClient(user.get());
        bid.setAuction(auction.get());
        bid.setBidAmount(bidDTO.getBidAmount());
        bid = bidRepository.save(bid);
        return BidDTO.fromBidToDto(bid);
    }

    @Override
    public void deleteBid(Long id) {
        if (!bidRepository.existsById(id)) {
            throw new ResourceAccessException("Bid not found");
        }
        bidRepository.deleteById(id);
    }

    @Override
    public List<BidDTO> getAllBidsByClientId(long clientId) {
        Optional<Client> clientOptional = clientRepository.findById(clientId);
        if (clientOptional.isPresent()) {
            Client client = clientOptional.get();
            List<Bid> bids = bidRepository.findByClient(client);
            return bids.stream()
                    .map(BidDTO::fromBidToDto)
                    .collect(Collectors.toList());
        }
        throw new ResourceNotFoundException("Cliente não encontrado com id " + clientId);
    }

    @Override
    public List<BidDTO> getAllBidsByAuctionId(long auctionId) {
        Optional<Auction> auctionOptional = auctionRepository.findById(auctionId);
        if (auctionOptional.isPresent()) {
            Auction auction = auctionOptional.get();
            List<Bid> bids = bidRepository.findByAuction(auction);
            return bids.stream()
                    .map(BidDTO::fromBidToDto)
                    .collect(Collectors.toList());
        }
        throw new ResourceNotFoundException("Leilão não encontrado com id " + auctionId);
    }
}
