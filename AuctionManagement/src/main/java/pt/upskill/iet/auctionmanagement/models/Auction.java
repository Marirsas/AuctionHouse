package pt.upskill.iet.auctionmanagement.models;
import jakarta.persistence.*;
import lombok.*;
import pt.upskill.iet.auctionmanagement.dto.AuctionDTO;

import java.util.Date;
import java.util.ArrayList;
import java.util.List;

@RequiredArgsConstructor
@NoArgsConstructor
@AllArgsConstructor
@Setter
@Getter
@Entity
public class Auction {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long id;

    @NonNull
    private long itemId;

    @NonNull
    private Date startDate;

    @NonNull
    private Date finalDate;


    public static Auction fromAuctionDTO(AuctionDTO auctionDTO) {
        return new Auction(auctionDTO.getAuctionId(), auctionDTO.getItemId(), auctionDTO.getStartDate(), auctionDTO.getFinalDate());
    }
}



