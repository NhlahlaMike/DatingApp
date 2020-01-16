import { Photo } from './photo';

export interface User {
  id: number;
  username: string;
  knownAs: string;
  age: string;
  gender: string;
  created: Date;
  lastActive: Date;
  photoUrl: string;
  city: string;
  country: string;
  interests?: string; // optional attribute must always be at the bottom
  introduction?: string;
  lookingFor?: string;
  photos?: Photo[];
}
