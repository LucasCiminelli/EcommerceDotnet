import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";

export const saveAddressInfo = createAsyncThunk(
  "shoppingCart/saveAddressInfo",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: { "Content-type": "application/json" },
      };

      const { data } = await axios.post(
        "/api/v1/Order/address",
        params,
        requestConfig
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  }
);
