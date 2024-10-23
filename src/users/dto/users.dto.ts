export interface IUserProfile {
  username: string;
  schoolId: string;
  basic?: {
    name: string;
    age: number;
    gender: string;
    ancestry: string;
    political: string;
    qualification: string;
    major: string;
    contact: string;
    profile: string;
  };
  selectedCourses?: number[];
  family?: {
    birth: string;
    living: string;
    zipCode: string;
    homePhone: string;
    address: string;
    livingInCity: boolean;
  };
  photo?: string;
}
