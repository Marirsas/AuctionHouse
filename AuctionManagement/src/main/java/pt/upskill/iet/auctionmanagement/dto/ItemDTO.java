package pt.upskill.iet.auctionmanagement.dto;

public class ItemDTO {
    private int id;
    private String name;
    private ItemStatusDTO itemStatus;
    private CategoryDTO category;


    // Getters e Setters

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public CategoryDTO getCategory() {
        return category;
    }

    public ItemStatusDTO getItemStatus() {
        return itemStatus;
    }
}

