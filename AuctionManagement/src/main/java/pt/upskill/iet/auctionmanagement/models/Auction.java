package pt.upskill.iet.auctionmanagement.models;
import io.micrometer.common.lang.NonNull;
import io.micrometer.common.lang.Nullable;
import jakarta.persistence.*;
import lombok.*;
import java.time.LocalDate;


@RequiredArgsConstructor
@AllArgsConstructor
@Setter
@Getter
@Entity
public class Auction {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long auctionId;

    @NonNull
    private int itemId;

    @NonNull
    private LocalDate startDate;

    @NonNull
    private LocalDate finalDate;

    @Nullable
    private boolean isOpen;


}



