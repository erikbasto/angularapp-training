import { Injectable } from '@angular/core';
import { CanDeactivate  } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import { ConfirmService } from '../services/confirm.service';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<MemberEditComponent> {

  constructor(private confirmService: ConfirmService) { }

  canDeactivate(component: MemberEditComponent): Observable<boolean>  {
    if (component.editForm?.dirty){
      return this.confirmService.confirm();
      
      //confirm('Are you sure you want to continue? Any unsaved changes will be lost');
    }
    return of(true);
  }
  
}
