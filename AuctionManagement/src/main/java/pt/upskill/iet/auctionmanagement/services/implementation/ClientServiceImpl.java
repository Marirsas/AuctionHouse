package pt.upskill.iet.auctionmanagement.services.implementation;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import pt.upskill.iet.auctionmanagement.dto.AuctionDTO;
import pt.upskill.iet.auctionmanagement.dto.BidDTO;
import pt.upskill.iet.auctionmanagement.dto.ClientDTO;
import pt.upskill.iet.auctionmanagement.exceptions.ResourceNotFoundException;
import pt.upskill.iet.auctionmanagement.models.Client;
import pt.upskill.iet.auctionmanagement.repositories.ClientRepository;
import pt.upskill.iet.auctionmanagement.services.ClientService;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
public class ClientServiceImpl implements ClientService {

    private final ClientRepository clientRepository;

    @Autowired
    public ClientServiceImpl(ClientRepository clientRepository) {
        this.clientRepository = clientRepository;
    }

    @Override
    public ClientDTO createClient(ClientDTO clientDTO) {
        // Converte ClientDTO para entidade Client e salva no banco
        Client client = ClientDTO.fromDTO(clientDTO);
        client = clientRepository.save(client);
        return ClientDTO.fromUserToDto(client);
    }

    @Override
    public ClientDTO getClientById(long clientId) {
        Optional<Client> clientOptional = clientRepository.findById(clientId);
        if (clientOptional.isPresent()) {
            return ClientDTO.fromUserToDto(clientOptional.get());
        }
        // Lança exceção se o client não for encontrado
        throw new ResourceNotFoundException("Cliente não encontrado com id " + clientId);
    }

    @Override
    public List<ClientDTO> getAllClients() {
        List<Client> clients = clientRepository.findAll();
        return clients.stream()
                .map(ClientDTO::fromUserToDto)
                .collect(Collectors.toList());
    }

    @Override
    public ClientDTO updateClient(long clientId, ClientDTO clientDTO) {
        Optional<Client> clientOptional = clientRepository.findById(clientId);
        if (clientOptional.isPresent()) {
            Client client = clientOptional.get();
            // Atualiza os campos do cliente
            client.setName(clientDTO.getName());
            client.setNif(clientDTO.getNif());
            client = clientRepository.save(client);
            return ClientDTO.fromUserToDto(client);
        }
        // Lança exceção se o client não for encontrado
        throw new ResourceNotFoundException("Cliente não encontrado com id " + clientId);
    }

    @Override
    public void deleteClient(long clientId) {
        Optional<Client> clientOptional = clientRepository.findById(clientId);
        if (clientOptional.isPresent()) {
            clientRepository.delete(clientOptional.get());
        } else {
            // Lança exceção se o client não for encontrado
            throw new ResourceNotFoundException("Cliente não encontrado com id " + clientId);
        }
    }
}
