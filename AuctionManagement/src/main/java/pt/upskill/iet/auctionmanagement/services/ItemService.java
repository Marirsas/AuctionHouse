package pt.upskill.iet.auctionmanagement.services;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatusCode;
import org.springframework.stereotype.Service;
import org.springframework.web.reactive.function.client.WebClient;
import pt.upskill.iet.auctionmanagement.dto.ItemDTO;
import pt.upskill.iet.auctionmanagement.dto.ItemStatusDTO;
import pt.upskill.iet.auctionmanagement.dto.SaleDTO;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;

@Service
public class ItemService {

    private final WebClient.Builder webClientBuilder;

    @Autowired
    public ItemService(WebClient.Builder webClientBuilder) {
        this.webClientBuilder = webClientBuilder.baseUrl("http://localhost:5245/api/");
    }

    // Método para consumir a API externa e obter as informações do item
    public ItemDTO getItemDetails(int itemId) {
        try {
            return webClientBuilder.build().get()
                    .uri("/items/{itemId}", itemId)
                    .retrieve()
                    .bodyToMono(ItemDTO.class)
                    .block();
        } catch (RuntimeException e) {
            if (e.getMessage().contains("404")) {
                throw new RuntimeException("Item não encontrado: " + itemId);
            } else if (e.getMessage().contains("500")) {
                throw new RuntimeException("Erro no servidor ao buscar item: " + itemId);
            } else {
                throw new RuntimeException("Erro ao buscar item: " + e.getMessage());
            }
        }
    }

    public ItemDTO[] getItems() {
        return webClientBuilder
                .build()
                .get()
                .uri("items/available")
                .retrieve()
                .bodyToMono(ItemDTO[].class).block();
    }

    public ItemDTO[] getItemsAvailable() {
        return webClientBuilder
                .build()
                .get()
                .uri("items/available")
                .retrieve()
                .bodyToMono(ItemDTO[].class).block();
    }

    public ItemDTO updateItemStatus(int itemId, ItemStatusDTO status) {
        return webClientBuilder.build().patch()
                .uri("/items/{id}/{status}", itemId, status)  // Suponha que esse seja o endpoint correto
                .retrieve()
                .bodyToMono(ItemDTO.class).block();  // Não espera um corpo na resposta
    }

    public void createSale(SaleDTO sale) {
        webClientBuilder.build()
                .post()
                .uri("sales")  // Suponha que esse seja o endpoint correto
                .bodyValue(sale)
                .retrieve()
                .bodyToMono(Void.class)
                .block();

    }

}

