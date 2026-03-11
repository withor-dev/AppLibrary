import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Layout from "../components/Layout";
import BooksPage from "../pages/BooksPage";
import AuthorsPage from "../pages/AuthorsPage";
import GenresPage from "../pages/GenresPage";

export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Navigate to="/books" replace />} />
          <Route path="books" element={<BooksPage />} />
          <Route path="authors" element={<AuthorsPage />} />
          <Route path="genres" element={<GenresPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
