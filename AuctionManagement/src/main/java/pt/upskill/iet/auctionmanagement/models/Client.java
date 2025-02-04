package pt.upskill.iet.auctionmanagement.models;

import jakarta.persistence.*;
import lombok.*;
import pt.upskill.iet.auctionmanagement.dto.ClientDTO;

import java.util.List;
import java.util.stream.Collectors;

@RequiredArgsConstructor
@NoArgsConstructor
@AllArgsConstructor
@Setter
@Getter
@Entity
public class Client {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long id;

    @NonNull
    private String name;

    @NonNull
    private String nif;

    @OneToMany(mappedBy = "client", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<Bid> bidList;

    public static Client fromClientDTO(ClientDTO clientDTO) {
        List<Bid> bidList = clientDTO.getBidList().stream()
                .map(BidDTO::toBid)
                .collect(Collectors.toList());
        return new Client(clientDTO.getClientId(), clientDTO.getName(), clientDTO.getNif(), bidList);
    }
}
