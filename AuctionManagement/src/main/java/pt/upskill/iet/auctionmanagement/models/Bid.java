package pt.upskill.iet.auctionmanagement.models;


import jakarta.persistence.*;
import lombok.*;
import pt.upskill.iet.auctionmanagement.dto.BidDTO;

import java.util.Date;

@RequiredArgsConstructor
@NoArgsConstructor
@AllArgsConstructor
@Setter
@Getter
@Entity
public class Bid {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long bidId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "auction_id", nullable = false)
    @NonNull
    private Auction auction;

    @ManyToOne
    @JoinColumn(name = "client_id")
    private Client client;

    @NonNull
    private double bidPrice;

    @NonNull
    private Date bidDate;

    public static Bid fromBidDTO(BidDTO bidDTO, Auction auction, Client client) {
        return new Bid(bidDTO.getBidId(), auction, client, bidDTO.getBidPrice(), bidDTO.getBidDate());
    }
}
