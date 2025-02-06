package pt.upskill.iet.auctionmanagement.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import pt.upskill.iet.auctionmanagement.models.Auction;

public interface AuctionRepository extends JpaRepository<Auction, Long> {
}
