package pt.upskill.iet.auctionmanagement.services;

import pt.upskill.iet.auctionmanagement.dto.BidDTO;

import java.util.List;

public interface BidService {
    void createBid(BidDTO bidDTO);

    List<BidDTO> getAllBids();

    BidDTO getBidById(Long id);

    BidDTO updateBid(Long id, BidDTO bidDTO);

    void deleteBid(Long id);

    //Obter todos os bids de um cliente
    List<BidDTO> getAllBidsByClientId(long clientId);

    List<BidDTO> getAllBidsByAuctionId(long auctionId);
}
