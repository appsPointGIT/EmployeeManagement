import React from 'react';
import { DataGrid } from '@mui/x-data-grid';
import Paper from '@mui/material/Paper';

const DataTable = ({ columns, data }) => {
    const paginationModel = { page: 0, pageSize: 5 };

    return (
        <Paper sx={{ maxHeight: '500px', width: '100%' }}>
            <DataGrid
                rows={data}
                columns={columns}
                initialState={{ pagination: { paginationModel } }}
                pageSizeOptions={[5, 10]}
                sx={{  maxHeight: '500px', border: 0 }}
            />
        </Paper>
    );
};

export default DataTable;
