package pt.upskill.iet.auctionmanagement.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.Setter;
import org.springframework.hateoas.RepresentationModel;
import pt.upskill.iet.auctionmanagement.models.Auction;
import pt.upskill.iet.auctionmanagement.models.Bid;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;


@Setter
@Getter
@RequiredArgsConstructor
@AllArgsConstructor
public class AuctionDTO extends RepresentationModel<AuctionDTO> {
    private long auctionId;
    private int itemId;
    private LocalDate startDate;
    private LocalDate finalDate;
    private boolean isOpen;



    public static Auction fromDTO(AuctionDTO auctionDTO) {
        return new Auction(auctionDTO.getAuctionId(), auctionDTO.getItemId(), auctionDTO.getStartDate(), auctionDTO.getFinalDate(), auctionDTO.isOpen());
    }

    public static AuctionDTO fromAuctionToDto(Auction auction) {
        return new AuctionDTO(auction.getAuctionId(), auction.getItemId(), auction.getStartDate(), auction.getFinalDate(), auction.isOpen());
    }
}
