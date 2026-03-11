// Arquivo: src/pages/BooksPage.tsx
import { useEffect, useState } from "react";
import { useBookStore } from "../store/useBookStore";
import { useAuthorStore } from "../store/useAuthorStore";
import { useGenreStore } from "../store/useGenreStore";
import type { Book } from "../models/Book";

export default function BooksPage() {
  const {
    books,
    isLoading: isLoadingBooks,
    fetchBooks,
    addBook,
    updateBook,
    deleteBook,
  } = useBookStore();

  const { authors, fetchAuthors } = useAuthorStore();
  const { genres, fetchGenres } = useGenreStore();

  const [showForm, setShowForm] = useState(false);
  const [isSaving, setIsSaving] = useState(false);

  const [editingId, setEditingId] = useState<string | null>(null);

  const [title, setTitle] = useState("");
  const [authorId, setAuthorId] = useState("");
  const [genreId, setGenreId] = useState("");

  useEffect(() => {
    fetchBooks();
    fetchAuthors();
    fetchGenres();
  }, [fetchBooks, fetchAuthors, fetchGenres]);

  const resetForm = () => {
    setShowForm(false);
    setEditingId(null);
    setTitle("");
    setAuthorId("");
    setGenreId("");
  };

  const handleEdit = (book: Book) => {
    const author = authors.find((a) => a.name === book.authorName);
    const genre = genres.find((g) => g.name === book.genreName);

    setTitle(book.title);
    setAuthorId(author ? author.id : "");
    setGenreId(genre ? genre.id : "");

    setEditingId(book.id);
    setShowForm(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!title.trim() || !authorId || !genreId) {
      alert("Preencha todos os campos!");
      return;
    }

    setIsSaving(true);
    try {
      if (editingId) {
        await updateBook({ id: editingId, title, authorId, genreId });
      } else {
        await addBook({ title, authorId, genreId });
      }
      resetForm();
    } catch (error) {
      console.error(error);
    } finally {
      setIsSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    if (window.confirm("Tem certeza que deseja excluir este livro?")) {
      try {
        await deleteBook(id);
      } catch (error) {
        console.error(error);
      }
    }
  };

  return (
    <div>
      <h2 className="page-title">Lista de Livros</h2>

      {!showForm && (
        <button
          className="btn btn-primary"
          style={{ marginBottom: "20px" }}
          onClick={() => setShowForm(true)}
        >
          + Novo Livro
        </button>
      )}

      {showForm && (
        <div className="form-card">
          {/* O título muda dinamicamente */}
          <h3>{editingId ? "Editar Livro" : "Cadastrar Livro"}</h3>
          <form onSubmit={handleSubmit}>
            <div className="form-group" style={{ marginTop: "15px" }}>
              <label>Título do Livro</label>
              <input
                type="text"
                className="form-control"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                placeholder="Ex: O Senhor dos Anéis"
                required
              />
            </div>

            <div className="form-group">
              <label>Autor</label>
              <select
                className="form-control"
                value={authorId}
                onChange={(e) => setAuthorId(e.target.value)}
                required
              >
                <option value="">Selecione um autor...</option>
                {authors.map((a) => (
                  <option key={a.id} value={a.id}>
                    {a.name}
                  </option>
                ))}
              </select>
            </div>

            <div className="form-group">
              <label>Gênero</label>
              <select
                className="form-control"
                value={genreId}
                onChange={(e) => setGenreId(e.target.value)}
                required
              >
                <option value="">Selecione um gênero...</option>
                {genres.map((g) => (
                  <option key={g.id} value={g.id}>
                    {g.name}
                  </option>
                ))}
              </select>
            </div>

            <div className="form-actions">
              <button
                type="submit"
                className="btn btn-primary"
                disabled={isSaving}
              >
                {isSaving ? "Salvando..." : "Salvar"}
              </button>
              <button
                type="button"
                className="btn btn-secondary"
                onClick={resetForm}
                disabled={isSaving}
              >
                Cancelar
              </button>
            </div>
          </form>
        </div>
      )}

      {isLoadingBooks ? (
        <p>Carregando livros...</p>
      ) : (
        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>Título</th>
                <th>Autor</th>
                <th>Gênero</th>
                <th style={{ textAlign: "center" }}>Ações</th>
              </tr>
            </thead>
            <tbody>
              {books.length === 0 ? (
                <tr>
                  <td colSpan={4} style={{ textAlign: "center" }}>
                    Nenhum livro cadastrado.
                  </td>
                </tr>
              ) : (
                books.map((book) => (
                  <tr key={book.id}>
                    <td>{book.title}</td>
                    <td>{book.authorName}</td>
                    <td>{book.genreName}</td>
                    <td
                      style={{
                        textAlign: "center",
                        display: "flex",
                        gap: "10px",
                        justifyContent: "center",
                      }}
                    >
                      <button
                        className="btn btn-secondary"
                        onClick={() => handleEdit(book)}
                      >
                        Editar
                      </button>

                      <button
                        className="btn btn-danger"
                        onClick={() => handleDelete(book.id)}
                      >
                        Excluir
                      </button>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
