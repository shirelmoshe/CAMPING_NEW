import React from "react";

export const CardSale = ({
  productsId,
  buyerName,
  cellphoneNumber,
  Email,
  buyerAddress,
  CompanyName,
  Product,
  Price,
  CampaignName,
}) => {
  return (
    <>
      <tr>
        <td>{buyerName}</td>
        <td>{cellphoneNumber}</td>
        <td>{Email}</td>
        <td>{buyerAddress}</td>
        <td>{CompanyName}</td>
        <td>{Product}</td>
        <td>{Price}</td>
        <td>{CampaignName}</td>
      </tr>
    </>
  );
};
