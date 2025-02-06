package pt.upskill.iet.auctionmanagement.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import pt.upskill.iet.auctionmanagement.models.Auction;
import pt.upskill.iet.auctionmanagement.models.Bid;
import pt.upskill.iet.auctionmanagement.models.Client;

import java.util.List;
import java.util.Optional;

@Repository
public interface BidRepository extends JpaRepository<Bid, Long> {
    Optional<Bid> findTopByAuctionOrderByBidAmountDesc(Auction auction);

    List<Bid> findByAuction(Auction auction);

    List<Bid> findByClient(Client client);

    @Query("SELECT DISTINCT b.auction FROM Bid b WHERE b.client.clientId = :clientId")
    List<Auction> findAuctionsByClientId(@Param("clientId") Long clientId);

    @Query("""
        SELECT b.auction 
        FROM Bid b 
        WHERE b.client.clientId = :clientId 
        AND b.bidAmount = (
            SELECT MAX(b2.bidAmount) 
            FROM Bid b2 
            WHERE b2.auction = b.auction
        )
        AND b.auction.isOpen = false
    """)
    List<Auction> findWonAuctionsByClientId(@Param("clientId") Long clientId);
}
