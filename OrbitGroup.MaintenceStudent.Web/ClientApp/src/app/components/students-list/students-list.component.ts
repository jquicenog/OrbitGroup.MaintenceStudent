import { StudentsService } from './../../services/students.service';
import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'app-students-list',
  templateUrl: './students-list.component.html',
  styleUrls: ['./students-list.component.css']
})
export class StudentsListComponent implements OnInit {

  @HostBinding('class') classes = 'row';
  students: any = [];

  constructor(private studentServices: StudentsService) { }

  ngOnInit() {
    this.getStudents();
  }

  searchStudent(id: string) {
    this.students = [];
    id ? this.getStudentById(id) : this.getStudents();
  }

  getStudents() {
    this.studentServices.getStudents().subscribe(
      res => {
        this.students = res;
      },
      err => console.log(err)
    );
  }

  deleteStudent(id: string) {
    if (confirm(`"Are you sure to delete the student with id ${id}?"`)) {
      this.studentServices.deleteStudent(id).subscribe(
        res => {
          console.log(res);
          this.getStudents();
        },
        err => {
          console.log(err);
        }
      );
    }
  }

  getStudentById(id: string) {
    this.studentServices.getStudentById(id).subscribe(
      res => {
        console.log(res);
        this.students.push(res);
      },
      err => {
        console.log(err);
      }
    );
  }

}
