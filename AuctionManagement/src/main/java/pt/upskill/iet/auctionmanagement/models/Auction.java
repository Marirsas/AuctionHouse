package pt.upskill.iet.auctionmanagement.models;
import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import io.micrometer.common.lang.NonNull;
import io.micrometer.common.lang.Nullable;
import jakarta.persistence.*;
import lombok.*;
import pt.upskill.iet.auctionmanagement.dto.AuctionDTO;
import pt.upskill.iet.auctionmanagement.dto.BidDTO;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.stream.Collectors;

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



