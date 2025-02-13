import {
  configureStore,
  ConfigureStore,
  getDefaultMiddleware,
} from "@reduxjs/toolkit";
import { productsReducer } from "./slices/productsSlice";


export default configureStore({
  reducer: {
    products: productsReducer
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({ serializableCheck: false }),
});
