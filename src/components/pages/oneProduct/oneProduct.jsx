import { useParams } from "react-router-dom";
import { getProductDataById } from "../../servers/servers";
import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";

export const ProductInfo = (productObject) => {
  // get the 'productsId' parameter from the URL
  const { productsId } = useParams();

  const [product, setProduct] = useState(undefined);
  const [error, setError] = useState(null);
  const [isLoaded, setIsLoaded] = useState(false);

  const initProductData = async () => {
    try {
      const product = await getProductDataById(productObject.productsId);
      setProduct(product);
      setIsLoaded(true);
    } catch (error) {
      setError(error);
      setIsLoaded(true);
    }
  };

  useEffect(() => {
    initProductData();
  }, []);

  if (error) {
    return <div>Error: {error.message}</div>;
  } else if (!isLoaded) {
    return <div>Loading...</div>;
  } else {
    return (
      <div className="card product-container">
        <div className="card-body">
          {product.ProductName && (
            <h2 className="card-title">{product.Product}</h2>
          )}

          {product.Product && <h5>Product ID: {product.productsId}</h5>}
          {product.Price && product.Price.Value && (
            <h5>Unit Price: {product.Price.Value}</h5>
          )}
          <Link to="/productsId/:{productsId}">
            <button className="btn btn-primary">Return To Products</button>
          </Link>
        </div>
      </div>
    );
  }
};
