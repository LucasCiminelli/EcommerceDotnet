import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";

export const getShoppingCart = createAsyncThunk(
  "shoppingCart/GetById",
  async ({ rejectWithValue }) => {
    try {
      const shoppingCartId =
        localStorage.getItem("shoppingCartId") ??
        "00000000-0000-0000-0000-000000000000";

      console.log("ID del carrito enviado:", shoppingCartId); // DEBUG

      const { data } = await axios.get(
        `/api/v1/ShoppingCart/${shoppingCartId}`
      );

      return data;
    } catch (error) {
      return rejectWithValue(`Errores: ${error.response.data.message}`);
    }
  }
);

export const addShoppingCartItem = createAsyncThunk(
  "shoppingCart/update",
  async (params, { rejectWithValue }) => {
    try {
      const { shoppingCartItems, item, cantidad } = params;
      let items = [];

      items = shoppingCartItems.slice(); //crear copia

      const indice = shoppingCartItems.findIndex(
        (x) => x.productId === item.productId
      );

      if (indice === -1) {
        items.push(item);
      } else {
        let cantidad_ = items[indice].cantidad;
        let total = cantidad_ + cantidad;
        let itemNewClone = { ...items[indice] };
        itemNewClone.cantidad = total;

        items[indice] = itemNewClone;
      }

      var request = {
        shoppingCartItems: items, //asignarle a shoppingCartItems el contenido de items, que contiene los shoppingCartsItems actualizados.
      };

      const requestConfig = {
        header: {
          "Content-Type": "application/json",
        },
      };

      const { data } = await axios.put(
        `/api/v1/ShoppingCart/${params.ShoppingCartId}`,
        request,
        requestConfig
      );

      return data;
    } catch (error) {
      return rejectWithValue(`Errores: ${error.response.data.message}`);
    }
  }
);

export const removeShoppingCartItem = createAsyncThunk(
  "shoppingCart/removeItem",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: { "Content-Type": "application/json" },
      };

      const { data } = await axios.delete(
        `/api/v1/ShoppingCart/item/${params.id}`,
        params,
        requestConfig
      );

      return data;
    } catch (error) {
      return rejectWithValue(`Errores: ${error.response.data.message}`);
    }
  }
);

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
