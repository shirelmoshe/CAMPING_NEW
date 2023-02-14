import React, { useState, useEffect } from "react";
import { getSalesData } from "../../servers/servers";
import { CardSale } from "../CardSales/CardSales";

export const SalesTable = () => {
  const [sales, setSales] = useState([]);
  const [filter, setFilter] = useState("");

  const initSaleTableInfo = async () => {
    let response = await getSalesData();
    if (response && typeof response === "object") {
      let CampaignsArr = Object.values(response);
      setSales(CampaignsArr);
    } else {
      console.log("error");
    }
  };

  useEffect(() => {
    initSaleTableInfo();
  }, []);

  const handleFilter = (e) => {
    setFilter(e.target.value);
  };

  const filteredSaleTable = sales.filter((card) => {
    return (
      card.buyerName.toLowerCase().includes(filter.toLowerCase()) ||
      card.cellphoneNumber.toLowerCase().includes(filter.toLowerCase()) ||
      card.Email.toLowerCase().includes(filter.toLowerCase()) ||
      card.buyerAddress.toLowerCase().includes(filter.toLowerCase()) ||
      card.CompanyName.toLowerCase().includes(filter.toLowerCase()) ||
      card.Product.toLowerCase().includes(filter.toLowerCase()) ||
      card.Price.toLowerCase().includes(filter.toLowerCase()) ||
      card.CampaignName.toLowerCase().includes(filter.toLowerCase())
    );
  });

  return (
    <>
      <div className="filter-container">
        <input
          type="text"
          placeholder="Filter"
          value={filter}
          onChange={handleFilter}
        />
      </div>
      <table className="table table-striped">
        <thead className="table table-striped">
          <tr>
            <th scope="col">buyerName</th>
            <th scope="col">cellphoneNumber</th>
            <th scope="col">Email</th>
            <th scope="col">buyerAddress</th>
            <th scope="col">CompanyName</th>
            <th scope="col">Product</th>
            <th scope="col">Price</th>
            <th scope="col">CampaignName</th>
          </tr>
        </thead>
        <tbody className="table table-striped">
          {filteredSaleTable.map((response) => {
            const {
              productsId,
              buyerName,
              cellphoneNumber,
              Email,
              buyerAddress,
              CompanyName,
              Product,
              Price,
              CampaignName,
            } = response;
            return (
              <CardSale
                key={productsId}
                buyerName={buyerName}
                cellphoneNumber={cellphoneNumber}
                Email={Email}
                buyerAddress={buyerAddress}
                CompanyName={CompanyName}
                Product={Product}
                Price={Price}
                CampaignName={CampaignName}
              ></CardSale>
            );
          })}
        </tbody>
      </table>
    </>
  );
};
