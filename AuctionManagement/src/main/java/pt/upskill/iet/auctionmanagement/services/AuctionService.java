package pt.upskill.iet.auctionmanagement.services;

import pt.upskill.iet.auctionmanagement.dto.AuctionDTO;
import pt.upskill.iet.auctionmanagement.dto.BidDTO;
import pt.upskill.iet.auctionmanagement.dto.ItemDTO;
import reactor.core.publisher.Flux;

import java.util.List;

public interface AuctionService {

    // Criar um novo leilão
    AuctionDTO createAuction(AuctionDTO auctionDTO);

    // Obter um leilão por ID
    AuctionDTO getAuctionById(long auctionId);

    // Obter todos os leilões
    List<AuctionDTO> getAllAuctions();

    // Excluir um leilão
    void deleteAuction(long auctionId);


    //Obter todos os leilões em que um cliente fez um bid
    List<AuctionDTO> getAuctionsByClient(long clientId);

    //Obter todos od leilões que um cliente ganhou
    List<AuctionDTO> getWonAuctionsByClient(long clientId);

    List<ItemDTO> getAvailableItems();

    // Atualizar um leilão existente
    // AuctionDTO updateAuction(long auctionId, AuctionDTO auctionDTO);
}

