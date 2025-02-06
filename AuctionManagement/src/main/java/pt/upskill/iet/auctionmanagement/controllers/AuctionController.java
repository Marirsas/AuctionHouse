package pt.upskill.iet.auctionmanagement.controllers;

import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import pt.upskill.iet.auctionmanagement.dto.AuctionDTO;
import pt.upskill.iet.auctionmanagement.exceptions.ResourceNotFoundException;
import pt.upskill.iet.auctionmanagement.services.AuctionService;

import java.util.List;

@RestController
@RequestMapping("/api/auctions")
public class AuctionController {

    private final AuctionService auctionService;

    public AuctionController(AuctionService auctionService) {
        this.auctionService = auctionService;
    }

    // Criar um novo leilão
    @PostMapping
    public ResponseEntity<AuctionDTO> createAuction(@Valid @RequestBody AuctionDTO auctionDTO) {
        AuctionDTO createdAuction = auctionService.createAuction(auctionDTO);
        return ResponseEntity.status(HttpStatus.CREATED).body(createdAuction);
    }

    // Obter um leilão por ID
    @GetMapping("/{auctionId}")
    public ResponseEntity<AuctionDTO> getAuctionById(@PathVariable long auctionId) {
        AuctionDTO auction = auctionService.getAuctionById(auctionId);
        return ResponseEntity.ok(auction);
    }

    // Obter todos os leilões
    @GetMapping
    public ResponseEntity<List<AuctionDTO>> getAllAuctions() {
        return ResponseEntity.ok(auctionService.getAllAuctions());
    }

    // Atualizar um leilão existente
    @PutMapping("/{auctionId}")
    public ResponseEntity<AuctionDTO> updateAuction(@PathVariable long auctionId, @Valid @RequestBody AuctionDTO auctionDTO) {
        AuctionDTO updatedAuction = auctionService.updateAuction(auctionId, auctionDTO);
        return ResponseEntity.ok(updatedAuction);
    }

    // Excluir um leilão
    @DeleteMapping("/{auctionId}")
    public ResponseEntity<Void> deleteAuction(@PathVariable long auctionId) {
        auctionService.deleteAuction(auctionId);
        return ResponseEntity.noContent().build();
    }

    // Tratamento global para ResourceNotFoundException
    @ExceptionHandler(ResourceNotFoundException.class)
    public ResponseEntity<String> handleResourceNotFound(ResourceNotFoundException ex) {
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(ex.getMessage());
    }

    // Tratamento para outras exceções
    @ExceptionHandler(Exception.class)
    public ResponseEntity<String> handleException(Exception ex) {
        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Erro interno: " + ex.getMessage());
    }
}

