package pt.upskill.iet.auctionmanagement.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import pt.upskill.iet.auctionmanagement.models.Auction;
import pt.upskill.iet.auctionmanagement.models.Bid;

import java.util.Optional;

public interface BidRepository extends JpaRepository<Bid, Long> {
    Optional<Bid> findTopByAuctionOrderByBidAmountDesc(Auction auction);
}
