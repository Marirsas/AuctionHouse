package pt.upskill.iet.auctionmanagement.dto;

import pt.upskill.iet.auctionmanagement.models.Bid;

import java.util.Date;

public class BidDTO {
    private long bidId;
    private long auctionId;
    private long clientId;
    private double bidPrice;
    private Date bidDate;

    public BidDTO(long bidId, long auctionId, long clientId, double bidPrice, Date bidDate) {
        this.bidId = bidId;
        this.auctionId = auctionId;
        this.clientId = clientId;
        this.bidPrice = bidPrice;
        this.bidDate = bidDate;
    }

    public void setBidId(long bidId) {
        this.bidId = bidId;
    }

    public void setAuctionId(long auctionId) {
        this.auctionId = auctionId;
    }

    public void setClientId(long clientId) {
        this.clientId = clientId;
    }

    public void setBidPrice(double bidPrice) {
        this.bidPrice = bidPrice;
    }

    public void setBidDate(Date bidDate) {
        this.bidDate = bidDate;
    }

    public long getBidId() {
        return bidId;
    }

    public long getAuctionId() {
        return auctionId;
    }

    public long getClientId() {
        return clientId;
    }

    public double getBidPrice() {
        return bidPrice;
    }

    public Date getBidDate() {
        return bidDate;
    }

    public static BidDTO toBidDTO(Bid bid) {
        return new BidDTO(bid.getBidId(), bid.getAuction().getId(), bid.getClient().getId(), bid.getBidPrice(), bid.getBidDate());
    }
}