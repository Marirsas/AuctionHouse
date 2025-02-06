package pt.upskill.iet.auctionmanagement.services;

import pt.upskill.iet.auctionmanagement.dto.ClientDTO;

import java.util.List;

public interface ClientService {

    ClientDTO createClient(ClientDTO clientDTO);

    ClientDTO getClientById(long clientId);

    List<ClientDTO> getAllClients();

    ClientDTO updateClient(long clientId, ClientDTO clientDTO);

    void deleteClient(long clientId);
}
