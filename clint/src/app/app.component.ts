import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ger3ah';
  names: any[];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/Ger3ah/GetAllGer3ahNames').subscribe((response: any) => {
      this.names = response;
      console.log(response);
    }, error => {
      console.log(error);
    })
  }
}
