export interface Book {
  id: string;
  title: string;
  authorName: string;
  genreName: string;
}

export interface BookCreateDTO {
  title: string;
  authorId: string;
  genreId: string;
}

export interface BookUpdateDTO {
  id: string;
  title: string;
  authorId: string;
  genreId: string;
}
