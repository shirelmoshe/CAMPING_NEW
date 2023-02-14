import axios from "axios";

const api = "http://localhost:7116/api/";
export const addDonation = async (userComment) => {
  console.log(userComment);

  await axios.post(api + "donation", userComment);
};
export const addSale = async (userComment) => {
  console.log(userComment);

  await axios.post(api + "addSale", userComment);
};

export const addCanpaignServers = async (userComment) => {
  console.log(userComment);

  await axios.post(api + "creatingCampaign", userComment);
};
export const addSupportServers = async (userComment) => {
  console.log(userComment);

  await axios.post(api + "Support", userComment);
};
export const addUserServers = async (userComment) => {
  console.log(userComment);

  await axios.post(api + "SnigUp", userComment);
};

export const getCampaigns = async () => {
  try {
    let results = await fetch(api + "Campaigns");
    if (!results.ok) {
      throw new Error(`HTTP error! status: ${results.status}`);
    }
    let response = await results.json();
    return response;
  } catch (error) {
    console.error("DONT");
  }
};

export const getProducts = async () => {
  try {
    let results = await fetch(api + "Products");
    if (!results.ok) {
      throw new Error(`HTTP error! status: ${results.status}`);
    }
    let response = await results.json();
    return response;
  } catch (error) {
    console.error("DONT");
  }
};

export const getShippingDataById = async (user_id) => {
  try {
    let endpoint = api + `Shipping/${user_id}`;
    let product = await axios.get(endpoint);
    console.log(product);
    return product.data;
  } catch (error) {
    console.error(error);
  }
};
/*
export const updateAdminData = async (userComment) => {
  console.log(userComment);

  await axios.post("donation", userComment);
};
*/
export const getsocialActivistDataById = async (user_id) => {
  try {
    let endpoint = api + `SocialActivists/${user_id}`;
    console.log(user_id);
    let user = await axios.get(endpoint);
    console.log(user_id);
    return user.data;
  } catch (error) {
    console.error(error);
  }
};
export const getCompanyOwnerUserDataById = async (user_id) => {
  try {
    let endpoint = api + `CompanyOwnerUser/${user_id}`;
    console.log(user_id);
    let user = await axios.get(endpoint);
    console.log(user_id);
    return user.data;
  } catch (error) {
    console.error(error);
  }
};

export const getAdminDataById = async (user_id) => {
  try {
    let endpoint = api + `getadmin/${user_id}`;
    console.log(user_id);
    let user = await axios.get(endpoint);
    console.log(user_id);
    return user.data;
  } catch (error) {
    console.error(error);
  }
};

export const getAssociationRepresentativeDataById = async (user_id) => {
  try {
    let endpoint = api + `AssociationRepresentative/${user_id}`;
    let user = await axios.get(endpoint);
    console.log(user);
    return user.data;
  } catch (error) {
    console.error(error);
  }
};

export const getProductDataById = async (productsId) => {
  try {
    let endpoint = api + `Products/${productsId}`;
    let product = await axios.get(endpoint);
    console.log(product);
    return product.data;
  } catch (error) {
    console.error(error);
  }
};
/*
export const UpdateDataById = async (userId, campaignToUpdate) => {
  try {
    let endpoint = api+`UpdatePage/${userId}`;
    let response = await axios.put(endpoint, campaignToUpdate);
    console.log(response);
    return response.data;
  } catch (error) {
    console.error(error);
  }
};
*/
export const getMoneyTrackingTable = async (user_id) => {
  try {
    let endpoint = api + `MoneyTracking/${user_id}`;

    let user = await axios.get(endpoint);
    console.log(user);
    return user.data;
  } catch (error) {
    console.error(error);
  }
};

export const CampaignTableByID = async (user_id) => {
  try {
    let endpoint = api + `CampaignTableID/${user_id}`;

    let user = await axios.get(endpoint);
    console.log(user);
    return user.data;
  } catch (error) {
    console.error(error);
  }
};

export const GetRoles = async (userId) => {
  let result = await axios.get(api + `roles/${userId}`);
  if (result.status === 200) {
    return result.data;
  } else {
    return {};
  }
};

export const getSalesData = async () => {
  try {
    let endpoint = api + "Sales";
    let Sales = await axios.get(endpoint);
    console.log(Sales);
    return Sales.data;
  } catch (error) {
    console.error(error);
  }
};
/*
export const getShipping = async () => {
  try {
    let endpoint = "http://localhost:7200/api/Function1/Shipping";
    let Sales = await axios.get(endpoint);
    console.log(Sales);
    return Sales.data;
  } catch (error) {
    console.error(error);
  }
};
*/
export const getCampaingsTable = async () => {
  try {
    let endpoint = api + "CampaingsTable";
    let Sales = await axios.get(endpoint);
    console.log(Sales);
    return Sales.data;
  } catch (error) {
    console.error(error);
  }
};

export const getTwitterTable = async () => {
  try {
    let endpoint = api + "TwitterTable";
    let Sales = await axios.get(endpoint);
    console.log(Sales);
    return Sales.data;
  } catch (error) {
    console.error(error);
  }
};

export const getUsersData = async () => {
  try {
    let endpoint = api + "User";
    let Sales = await axios.get(endpoint);
    console.log(Sales);
    return Sales.data;
  } catch (error) {
    console.error(error);
  }
};
export const UpdateCampingAsync = async (updateCamping) => {
  console.log(` : ${updateCamping.userId}`);
  const res = await axios.put(
    `UpdatePage/${updateCamping.userId}`,
    updateCamping
  );
  console.log(res);
};
export const deleteCampungAsync = async (userId) => {
  console.log(` deleteCamping: ${userId}`);
  const res = await axios.delete(api + `deleteCamping/${userId}`);
  console.log(res);
};
