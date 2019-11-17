import { Student } from './../models/student';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {
  API_URI = 'http://localhost:52457/api';

  constructor(private http: HttpClient) { }

  getStudents() {
    return this.http.get(`${this.API_URI}/student`);
  }

  deleteStudent(id: string) {
    return this.http.delete(`${this.API_URI}/student/${id}`);
  }

  getStudentById(id: string) {
    return this.http.get(`${this.API_URI}/student/${id}`);
  }

  saveStudent(student: Student) {
    return this.http.post(`${this.API_URI}/student`, student);
  }

  updateStudent(student: Student) {
    return this.http.put(`${this.API_URI}/student`, student);
  }
}
