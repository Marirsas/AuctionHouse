package pt.upskill.iet.auctionmanagement.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.Setter;
import pt.upskill.iet.auctionmanagement.models.Bid;
import pt.upskill.iet.auctionmanagement.models.Client;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

@Getter
@Setter
@AllArgsConstructor
@RequiredArgsConstructor
public class ClientDTO {
    private long clientId;
    private String name;
    private String nif;


    public static ClientDTO fromUserToDto(Client client) {
        return new ClientDTO(client.getClientId(), client.getName(), client.getNif());

    }

    public static Client fromDTO(ClientDTO clientDTO) {
        return new Client(clientDTO.getClientId(), clientDTO.getName(), clientDTO.getNif());
    }
}