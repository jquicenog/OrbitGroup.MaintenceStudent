import { Student } from './../../models/student';
import { Component, OnInit, HostBinding } from '@angular/core';
import { StudentsService } from 'src/app/services/students.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-student-form',
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.css']
})
export class StudentFormComponent implements OnInit {
  @HostBinding('class') classes = 'row';
  student: Student = {
    id: 0,
    userName: '',
    firstName: '',
    lastName: '',
    age: 0,
    career: ''
  };

  edit = false;
  constructor(private studentService: StudentsService, private route: Router, private activeRoute: ActivatedRoute) { }

  ngOnInit() {
    const params = this.activeRoute.snapshot.params;
    if (params.id) {
      this.studentService.getStudentById(params.id).subscribe(
        res => {
          console.log(res);
          this.student = res;
          this.edit = true;
        },
        err => {
          console.log(err);
        }
      );
    }
    console.log(params);
  }

  saveNewStudent() {
    delete this.student.id;

    this.studentService.saveStudent (this.student).subscribe(
      res => {
        console.log(res);
        this.route.navigate(['/students']);
      },
      err => {
        console.log(err);
      }
    );
  }

  updateStudent() {
    this.studentService.updateStudent(this.student).subscribe(
      res => {
        console.log(res);
        this.route.navigate(['/students']);
      },
      err => {
        console.log(err);
      }
    );
  }

  cancel() {
    this.route.navigate(['/students']);
  }

}
