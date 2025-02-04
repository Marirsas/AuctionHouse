package pt.upskill.iet.auctionmanagement.dto;

import pt.upskill.iet.auctionmanagement.models.Client;

import java.util.List;
import java.util.stream.Collectors;

public class ClientDTO {
    private long clientId;
    private String name;
    private String nif;
    private List<BidDTO> bidList;

    public ClientDTO(long clientId, String name, String nif, List<BidDTO> bidList) {
        this.clientId = clientId;
        this.name = name;
        this.nif = nif;
        this.bidList = bidList;
    }

    public long getClientId() {
        return clientId;
    }

    public void setClientId(long clientId) {
        this.clientId = clientId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getNif() {
        return nif;
    }

    public void setNif(String nif) {
        this.nif = nif;
    }

    public List<BidDTO> getBidList() {
        return bidList;
    }

    public void setBidList(List<BidDTO> bidList) {
        this.bidList = bidList;
    }

    public static ClientDTO toClientDTO(Client client) {
        List<BidDTO> bidDTOList = client.getBidList().stream()
                .map(BidDTO::toBidDTO)
                .collect(Collectors.toList());
        return new ClientDTO(client.getId(), client.getName(), client.getNif(), bidDTOList);
    }
}