package pt.upskill.iet.auctionmanagement.services;

import pt.upskill.iet.auctionmanagement.dto.AuctionDTO;

import java.util.List;

public interface AuctionService {

    // Criar um novo leilão
    AuctionDTO createAuction(AuctionDTO auctionDTO);

    // Obter um leilão por ID
    AuctionDTO getAuctionById(long auctionId);

    // Obter todos os leilões
    List<AuctionDTO> getAllAuctions();

    // Atualizar um leilão existente
    AuctionDTO updateAuction(long auctionId, AuctionDTO auctionDTO);

    // Excluir um leilão
    void deleteAuction(long auctionId);
}

