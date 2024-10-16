export interface ICourse {
  id: number;
  name: string;
  qualification: 'undergraduate' | 'bachelor' | 'doctor';
  major?: string;
  description?: string;
}
