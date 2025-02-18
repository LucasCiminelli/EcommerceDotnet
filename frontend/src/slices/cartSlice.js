import { createSlice } from "@reduxjs/toolkit";
import {
  addShoppingCartItem,
  getShoppingCart,
  removeShoppingCartItem,
} from "../actions/cartAction";

const initialState = {
  shoppingCartId: "",
  shoppingCartItems: [],
  loading: false,
  error: null,
  total: 0,
  subtotal: 0,
  cantidad: 0,
  impuesto: 0,
  precioEnvio: 0,
};

export const cartSlice = createSlice({
  name: "cartItems",
  initialState,
  reducers: {},
  extraReducers: {
    [getShoppingCart.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [getShoppingCart.fulfilled]: (state, { payload }) => {
      console.log(payload);
      localStorage.setItem("shoppingCartId", payload.shoppingCartId);

      state.loading = false;
      state.error = null;
      state.shoppingCartId = payload.shoppingCartId;
      state.shoppingCartItems = payload.shoppingCartItems ?? [];
      state.total = payload.total;
      state.subtotal = payload.subtotal;
      state.cantidad = payload.cantidad;
      state.impuesto = payload.impuesto;
      state.precioEnvio = payload.precioEnvio;
    },
    [getShoppingCart.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },
    [addShoppingCartItem.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [addShoppingCartItem.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.error = null;
      state.shoppingCartId = payload.shoppingCartId;
      state.shoppingCartItems = payload.shoppingCartItems ?? [];
      state.total = payload.total;
      state.subtotal = payload.subtotal;
      state.cantidad = payload.cantidad;
      state.impuesto = payload.impuesto;
      state.precioEnvio = payload.precioEnvio;
    },
    [addShoppingCartItem.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },
    [removeShoppingCartItem.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [removeShoppingCartItem.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.error = null;
      state.shoppingCartId = payload.shoppingCartId;
      state.shoppingCartItems = payload.shoppingCartItems ?? [];
      state.total = payload.total;
      state.subtotal = payload.subtotal;
      state.cantidad = payload.cantidad;
      state.impuesto = payload.impuesto;
      state.precioEnvio = payload.precioEnvio;
    },
    [removeShoppingCartItem.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },
  },
});

export const cartReducer = cartSlice.reducer;
