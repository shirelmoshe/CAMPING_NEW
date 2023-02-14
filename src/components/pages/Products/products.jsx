import React, { useState, useEffect, useContext } from "react";

import { getProducts } from "./../../servers/servers";
import { CardProducts } from "../../cardproducts/cardProducts";
import { UserContext } from "../../context/context";
import { ThemeContext } from "@emotion/react";

export const Products = () => {
  const { setProduct } = useContext(ThemeContext);
  const { setProductUp } = useContext(UserContext);
  const [arrProducts, setArrProducts] = useState([]);
  const [filter, setFilter] = useState("");

  let getProducts1 = async () => {
    let ProductsArr = await getProducts();
    let arrPro = Object.values(ProductsArr);
    setArrProducts(arrPro);
  };

  useEffect(() => {
    getProducts1();
  }, []);

  const productDataUrl = (objectProduct) => {
    console.log("objectProduct", objectProduct);
    setProduct(objectProduct);
  };

  const filteredProducts = arrProducts.filter((product) =>
    product.CampaignName.toLowerCase().includes(filter.toLowerCase())
  );

  return (
    <>
      <input
        type="text"
        value={filter}
        onChange={(e) => setFilter(e.target.value)}
      />
      {filteredProducts.length > 0
        ? filteredProducts.map((p) => {
            return (
              <>
                <CardProducts
                  btnLink={productDataUrl}
                  productObject={p}
                  changeSetfunction={setProductUp}
                />
              </>
            );
          })
        : "loading"}
    </>
  );
};
