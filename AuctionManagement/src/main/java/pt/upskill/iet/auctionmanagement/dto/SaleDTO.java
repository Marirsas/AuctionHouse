package pt.upskill.iet.auctionmanagement.dto;

public class SaleDTO {
    private double salePrice;
    private int itemId;

    public SaleDTO(int itemId, double salePrice) {
        this.salePrice = salePrice;
        this.itemId = itemId;
    }

    public double getSalePrice() {
        return salePrice;
    }

    public int getItemId() {
        return itemId;
    }

    public void setSalePrice(double salePrice) {
        this.salePrice = salePrice;
    }

    public void setItemId(int itemId) {
        this.itemId = itemId;
    }
}
