package pt.upskill.iet.auctionmanagement.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.client.ResourceAccessException;
import pt.upskill.iet.auctionmanagement.dto.BidDTO;
import pt.upskill.iet.auctionmanagement.services.BidService;

import java.util.List;

@RestController
@RequestMapping("/api/bids")
public class BidController {
    @Autowired
    private final BidService bidService;

    public BidController(BidService bidService) {
        this.bidService = bidService;
    }

    // Criar um novo lance
    @PostMapping
    public ResponseEntity<String> createBid(@RequestBody BidDTO bidDTO) {
        try {
            bidService.createBid(bidDTO);
            return ResponseEntity.status(HttpStatus.CREATED).body("Bid created successfully.");
        } catch (IllegalArgumentException e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage());
        } catch(ResourceAccessException e) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(e.getMessage());
        }

    }

    // Buscar todos os lances
    @GetMapping
    public ResponseEntity<List<BidDTO>> getAllBids() {
        return ResponseEntity.ok(bidService.getAllBids());
    }

    // Buscar um lance por ID
    @GetMapping("/{id}")
    public ResponseEntity<BidDTO> getBidById(@PathVariable Long id) {
        try {
            return ResponseEntity.ok(bidService.getBidById(id));
        } catch (IllegalArgumentException e) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).build();
        }
    }

    // Atualizar um lance existente
    @PutMapping("/{id}")
    public ResponseEntity<BidDTO> updateBid(@PathVariable Long id, @RequestBody BidDTO bidDTO) {
        try {
            return ResponseEntity.ok(bidService.updateBid(id, bidDTO));
        } catch (ResourceAccessException e) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).build();
        }
    }

    // Excluir um lance
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteBid(@PathVariable Long id) {
        try {
            bidService.deleteBid(id);
            return ResponseEntity.noContent().build();
        } catch (IllegalArgumentException e) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).build();
        }
    }

    @GetMapping("/client/{clientId}")
    public ResponseEntity<List<BidDTO>> getAllBidsByClientId(@PathVariable long clientId) {
        List<BidDTO> bids = bidService.getAllBidsByClientId(clientId);
        return ResponseEntity.ok(bids);
    }

    @GetMapping("/auction/{auctionId}")
    public ResponseEntity<List<BidDTO>> getAllBidsByAuctionId(@PathVariable long auctionId) {
        List<BidDTO> bids = bidService.getAllBidsByAuctionId(auctionId);
        return ResponseEntity.ok(bids);
    }
}
