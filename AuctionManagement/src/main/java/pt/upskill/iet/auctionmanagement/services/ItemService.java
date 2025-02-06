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
    public Mono<ItemDTO> getItemDetails(int itemId) {
        return webClientBuilder.build().get()
                .uri("/items/{itemId}", itemId)
                .retrieve()
                .onStatus(HttpStatusCode::is4xxClientError, response ->
                        Mono.error(new RuntimeException("Item não encontrado: " + itemId)))  // Trata erro 404
                .onStatus(HttpStatusCode::is5xxServerError, response ->
                        Mono.error(new RuntimeException("Erro no servidor ao buscar item: " + itemId)))  // Trata erro 500
                .bodyToMono(ItemDTO.class)
                .doOnError(error -> System.err.println("Erro ao buscar item: " + error.getMessage())); // Log do erro
    }

    public Flux<ItemDTO> getItems() {
        return webClientBuilder
                .build()
                .get()
                .uri("items/available")
                .retrieve()
                .bodyToFlux(ItemDTO.class);
    }

    public Flux<ItemDTO> getItemsAvailable() {
        return webClientBuilder
                .build()
                .get()
                .uri("items/available")
                .retrieve()
                .bodyToFlux(ItemDTO.class);
    }

    public Mono<Void> updateItemStatus(int itemId, ItemStatusDTO status) {
        return webClientBuilder.build().patch()
                .uri("/items/{id}/{status}", itemId, status)  // Suponha que esse seja o endpoint correto
                .retrieve()
                .bodyToMono(Void.class);  // Não espera um corpo na resposta
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

