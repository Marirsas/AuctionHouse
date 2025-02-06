package pt.upskill.iet.auctionmanagement.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import pt.upskill.iet.auctionmanagement.models.Client;

@Repository
public interface ClientRepository extends JpaRepository<Client, Long> {
}
