package pt.upskill.iet.auctionmanagement.dto;


import org.springframework.hateoas.RepresentationModel;
import pt.upskill.iet.auctionmanagement.models.Auction;

import java.util.Date;


public class AuctionDTO extends RepresentationModel<AuctionDTO> {

    private long auctionId;
    private long itemId;
    private Date startDate;
    private Date finalDate;

    public AuctionDTO(long auctionId, long itemId, Date startDate, Date finalDate) {
        this.auctionId = auctionId;
        this.itemId = itemId;
        this.startDate = startDate;
        this.finalDate = finalDate;
    }

    public void setAuctionId(long auctionId) {
        this.auctionId = auctionId;
    }

    public void setItemId(long itemId) {
        this.itemId = itemId;
    }

    public void setStartDate(Date startDate) {
        this.startDate = startDate;
    }

    public void setFinalDate(Date finalDate) {
        this.finalDate = finalDate;
    }

    public long getAuctionId() {
        return auctionId;
    }

    public long getItemId() {
        return itemId;
    }

    public Date getStartDate() {
        return startDate;
    }

    public Date getFinalDate() {
        return finalDate;
    }

    public static AuctionDTO toAuctionDTO(Auction auction) {
        return new AuctionDTO(auction.getId(), auction.getItemId(), auction.getStartDate(), auction.getFinalDate());
    }
}
