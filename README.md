# tiveriad-multitenancy
Multinenancy application



TODO


// TODO
// Add a new client
post http://{{hostname}}:{{port}}/api/organizations/1/clients
Content-Type: application/json
{

}

// Add a new role for the client
post http://{{hostname}}:{{port}}/api/organizations/1/clients/1/roles
Content-Type: application/json
{

}

// Get all roles for the organization and the client
get http://{{hostname}}:{{port}}/api/organizations/1/clients/1/roles

//delete roles for the user for the organization and the client
delete http://{{hostname}}:{{port}}/api/organizations/1/clients/1/roles
Content-Type: application/json
{

}


// Add a roles for the user for the organization and the client
post http://{{hostname}}:{{port}}/api/organizations/1/clients/1/users/1/roles
Content-Type: application/json
{

}

// Get all roles for the user for the organization and the client
get http://{{hostname}}:{{port}}/api/organizations/1/users/1/clients/1/roles



//delete roles for the user for the organization and the client
delete http://{{hostname}}:{{port}}/api/organizations/1/users/1/clients/1/roles
Content-Type: application/json
{

}

