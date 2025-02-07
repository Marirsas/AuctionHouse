package pt.upskill.iet.auctionmanagement.models;
import com.fasterxml.jackson.annotation.JsonIgnore;
import jakarta.persistence.*;
import lombok.*;
import java.time.LocalDate;

@RequiredArgsConstructor
@NoArgsConstructor
@AllArgsConstructor
@Setter
@Getter
@Entity
public class Bid {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long bidId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "auction_id", nullable = false)
    @NonNull
    private Auction auction;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "client_id", nullable = false)
    @JsonIgnore
    private Client client;

    @NonNull
    private double bidAmount;

    @NonNull
    private LocalDate bidDate;



}
