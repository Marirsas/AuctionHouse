package pt.upskill.iet.auctionmanagement.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import pt.upskill.iet.auctionmanagement.models.Auction;

@Repository
public interface AuctionRepository extends JpaRepository<Auction, Long> {


}
