import { Routes } from '@angular/router';
import { RegisterComponent } from '../components/register/register.component';
import { LoginComponent } from '../components/login/login.component';
import { DashboardComponent } from '../components/dashboard/dashboard.component';
import { ProfileComponent } from '../components/profile/profile.component';
import { TransactionsComponent } from '../components/transactions/transactions.component';
import { authGuard } from '../components/guard/auth.guard';
import { PayComponent } from '../components/pay/pay.component';
import { AdminComponent } from '../components/admin/admin.component';

export const routes: Routes = [
    {
        path:'',
        redirectTo:'login',
        pathMatch: 'full'
    },
    {
        path:'register',
        component: RegisterComponent
    },
    {
        path:'login',
        component:LoginComponent
    },
    {
        path:'dashboard',
        component: DashboardComponent,
        canActivate: [authGuard],
        children: [
            {
                path: '',
                redirectTo: 'profile',
                pathMatch: 'full'
            },
            {
                path: 'profile',
                component: ProfileComponent
            },
            {
                path: 'transactions',
                component: TransactionsComponent
            },
            {
                path: 'pay',
                component: PayComponent
            },
            {
                path: 'admin',
                component: AdminComponent
            }
        ]
    }
];

