package pt.upskill.iet.auctionmanagement.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import pt.upskill.iet.auctionmanagement.models.Client;

public interface ClientRepository extends JpaRepository<Client, Long> {
}
