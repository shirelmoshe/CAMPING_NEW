import React from "react";
import { Link } from "react-router-dom";
import { deleteCampungAsync } from "../../servers/servers";
export const CampaignTableIDCard = ({
  btnLink,
  campingObject,
  changeSetfunction,
}) => {
  let { userId, associationName, email, uri, hashtag, CampaignName } =
    campingObject;

  const deleteProduct = async (userId) => {
    await deleteCampungAsync(userId);
  };
  return (
    <>
      <tr>
        <td>{associationName}</td>
        <td>{email}</td>
        <td>{uri}</td>
        <td>{hashtag}</td>
        <td>{CampaignName}</td>
        <td>
          <Link
            to="/UpdatePage"
            className="btn btn-primary"
            onClick={() => {
              changeSetfunction(campingObject);
            }}
          >
            Update
          </Link>
        </td>
        <td>
          <button
            className="btn btn-primary"
            onClick={() => deleteProduct(userId)}
          >
            Delete
          </button>
        </td>
      </tr>
    </>
  );
};
