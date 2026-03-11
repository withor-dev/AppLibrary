import { Link, Outlet } from "react-router-dom";

export default function Layout() {
  return (
    <div className="container">
      <header className="header">
        <h1>📚 Gestão de Biblioteca</h1>
        <nav className="nav-links">
          <Link to="/books">Livros</Link>
          <Link to="/authors">Autores</Link>
          <Link to="/genres">Gêneros</Link>
        </nav>
      </header>

      <main>
        <Outlet />
      </main>
    </div>
  );
}
