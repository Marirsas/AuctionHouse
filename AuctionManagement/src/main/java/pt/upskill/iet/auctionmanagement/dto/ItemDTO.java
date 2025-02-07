package pt.upskill.iet.auctionmanagement.dto;

import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@RequiredArgsConstructor
public class ItemDTO {
    private int id;
    private String name;
    private ItemStatusDTO itemStatus;
    private int categoryId;
    private double initialPrice;
}

