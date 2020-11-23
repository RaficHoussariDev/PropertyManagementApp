import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { ApartmentDetailComponent } from '../apartment-detail/apartment-detail.component';

@Injectable()
export class PreventUnsavedChanges implements CanDeactivate<ApartmentDetailComponent> {
    canDeactivate(component: ApartmentDetailComponent) {
        if (component.editForm.dirty) {
            return confirm('Are you sure you want to continue? Any unsaved changes will be lost');
        }
        return true;
    }
}