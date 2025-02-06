package pt.upskill.iet.auctionmanagement.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.Setter;
import pt.upskill.iet.auctionmanagement.models.Auction;
import pt.upskill.iet.auctionmanagement.models.Bid;
import pt.upskill.iet.auctionmanagement.models.Client;
import pt.upskill.iet.auctionmanagement.repositories.AuctionRepository;
import pt.upskill.iet.auctionmanagement.repositories.ClientRepository;

import java.time.LocalDate;


@Getter
@Setter
@AllArgsConstructor
@RequiredArgsConstructor
public class BidDTO {
    private long id;
    private long auctionId;
    private long clientId;
    private double bidAmount;
    private LocalDate bidDate;


    // Método que converte de Bid para DTO
    public static BidDTO fromBidToDto(Bid bid) {
        return new BidDTO(
                bid.getBidId(),
                bid.getAuction().getAuctionId(),// O primeiro argumento deve ser o ID do lance
                bid.getClient().getClientId(),
                bid.getBidAmount(),
                bid.getBidDate()
        );
    }

    // Método que converte de DTO para Bid
    public static Bid fromDtoToBid(AuctionRepository auctionRepository, ClientRepository userRepository, BidDTO bidDTO) {
        Client user = userRepository.findById(bidDTO.getClientId())
                .orElseThrow(() -> new IllegalArgumentException("User not found with ID: " + bidDTO.getClientId()));

        Auction auction = auctionRepository.findById(bidDTO.getAuctionId())
                .orElseThrow(() -> new IllegalArgumentException("Auction not found with ID: " + bidDTO.getAuctionId()));

        return new Bid(auction, user, bidDTO.bidAmount,bidDTO.bidDate); // 0 para ID e itemId
    }

}